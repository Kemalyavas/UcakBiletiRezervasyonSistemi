using System;
using System.Collections.Generic;

namespace UcakBiletiRezervasyonSistemi
{
    // Ana sistem sınıfı - Tüm işlemleri yönetir
    public class RezervasyonSistemi
    {
        // Sistem verileri (bellekte tutuluyor - finalde dosyaya kaydedilecek)
        private List<Ucus> _ucuslar;
        private List<Rezervasyon> _rezervasyonlar;
        private List<Ucak> _ucaklar;
        private Admin _admin;

        // Constructor
        public RezervasyonSistemi()
        {
            _ucuslar = new List<Ucus>();
            _rezervasyonlar = new List<Rezervasyon>();
            _ucaklar = new List<Ucak>();

            // Varsayılan admin oluştur
            _admin = new Admin(1, "Admin", "Yönetici", "admin@sistem.com", "0000000000", 1);

            // Örnek veriler oluştur
            OrnekVerileriOlustur();
        }

        // Örnek uçak ve uçuş verileri
        private void OrnekVerileriOlustur()
        {
            // Örnek uçaklar
            Ucak ucak1 = new Ucak(1, "Boeing 737", 180, "3-3");
            Ucak ucak2 = new Ucak(2, "Airbus A320", 150, "3-3");
            Ucak ucak3 = new Ucak(3, "Boeing 777", 300, "3-3");
            _ucaklar.Add(ucak1);
            _ucaklar.Add(ucak2);
            _ucaklar.Add(ucak3);

            // Örnek uçuşlar
            _ucuslar.Add(new Ucus("TK101", "İstanbul", "Ankara", DateTime.Now.AddDays(1),
                new TimeSpan(8, 0, 0), ucak1, 850));
            _ucuslar.Add(new Ucus("TK202", "İstanbul", "İzmir", DateTime.Now.AddDays(1),
                new TimeSpan(10, 30, 0), ucak2, 750));
            _ucuslar.Add(new Ucus("TK303", "Ankara", "Antalya", DateTime.Now.AddDays(2),
                new TimeSpan(14, 0, 0), ucak1, 950));
            _ucuslar.Add(new Ucus("TK404", "İzmir", "Trabzon", DateTime.Now.AddDays(3),
                new TimeSpan(16, 30, 0), ucak2, 1100));
            _ucuslar.Add(new Ucus("TK505", "İstanbul", "Antalya", DateTime.Now.AddDays(2),
                new TimeSpan(9, 0, 0), ucak3, 1250));
        }

        // ==================== UÇUŞ İŞLEMLERİ ====================

        // Tüm uçuşları listele
        public void UcuslariListele()
        {
            Console.WriteLine("\n" + new string('=', 80));
            Console.WriteLine("                         TÜM UÇUŞLAR");
            Console.WriteLine(new string('=', 80));

            if (_ucuslar.Count == 0)
            {
                Console.WriteLine("Sistemde kayıtlı uçuş bulunmamaktadır.");
                return;
            }

            int sira = 1;
            foreach (var ucus in _ucuslar)
            {
                Console.WriteLine($"{sira}. {ucus.BilgiGoster()}");
                sira++;
            }
            Console.WriteLine(new string('=', 80));
        }

        // Uçuş ara (kalkış, varış ve tarihe göre)
        public List<Ucus> UcusAra(string kalkis, string varis, DateTime? tarih = null)
        {
            List<Ucus> sonuclar = new List<Ucus>();

            foreach (var ucus in _ucuslar)
            {
                bool kalkisUygun = string.IsNullOrEmpty(kalkis) ||
                    ucus.KalkisYeri.ToLower().Contains(kalkis.ToLower());
                bool varisUygun = string.IsNullOrEmpty(varis) ||
                    ucus.VarisYeri.ToLower().Contains(varis.ToLower());
                bool tarihUygun = !tarih.HasValue ||
                    ucus.KalkisTarihi.Date == tarih.Value.Date;

                if (kalkisUygun && varisUygun && tarihUygun && ucus.BosKoltukSayisi > 0)
                {
                    sonuclar.Add(ucus);
                }
            }

            return sonuclar;
        }

        // Uçuş arama menüsü
        public void UcusAraMenu()
        {
            Console.WriteLine("\n=== UÇUŞ ARAMA ===");

            Console.Write("Kalkış Yeri (boş bırakılabilir): ");
            string kalkis = Console.ReadLine();

            Console.Write("Varış Yeri (boş bırakılabilir): ");
            string varis = Console.ReadLine();

            Console.Write("Tarih (GG.AA.YYYY formatında, boş bırakılabilir): ");
            string tarihStr = Console.ReadLine();
            DateTime? tarih = null;

            if (!string.IsNullOrEmpty(tarihStr))
            {
                if (DateTime.TryParse(tarihStr, out DateTime parsedTarih))
                {
                    tarih = parsedTarih;
                }
                else
                {
                    Console.WriteLine("Geçersiz tarih formatı!");
                    return;
                }
            }

            List<Ucus> sonuclar = UcusAra(kalkis, varis, tarih);

            if (sonuclar.Count == 0)
            {
                Console.WriteLine("\nArama kriterlerine uygun uçuş bulunamadı.");
                return;
            }

            Console.WriteLine($"\n{sonuclar.Count} uçuş bulundu:");
            Console.WriteLine(new string('-', 80));

            int sira = 1;
            foreach (var ucus in sonuclar)
            {
                Console.WriteLine($"{sira}. {ucus.BilgiGoster()}");
                sira++;
            }

            // Rezervasyon yapmak ister mi?
            Console.Write("\nRezervasyon yapmak için uçuş numarası girin (0 = İptal): ");
            if (int.TryParse(Console.ReadLine(), out int secim) && secim > 0 && secim <= sonuclar.Count)
            {
                RezervasyonOlustur(sonuclar[secim - 1]);
            }
        }

        // Admin: Yeni uçuş ekle
        public void UcusEkle()
        {
            Console.WriteLine("\n=== YENİ UÇUŞ EKLE ===");

            try
            {
                Console.Write("Uçuş No (örn: TK606): ");
                string ucusNo = Console.ReadLine();

                Console.Write("Kalkış Yeri: ");
                string kalkis = Console.ReadLine();

                Console.Write("Varış Yeri: ");
                string varis = Console.ReadLine();

                Console.Write("Kalkış Tarihi (GG.AA.YYYY): ");
                DateTime tarih = DateTime.Parse(Console.ReadLine());

                Console.Write("Kalkış Saati (SS:DD): ");
                TimeSpan saat = TimeSpan.Parse(Console.ReadLine());

                Console.Write("Bilet Fiyatı (TL): ");
                decimal fiyat = decimal.Parse(Console.ReadLine());

                // Uçak seçimi
                Console.WriteLine("\nMevcut Uçaklar:");
                for (int i = 0; i < _ucaklar.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {_ucaklar[i].BilgiGoster()}");
                }
                Console.Write("Uçak seçin: ");
                int ucakSecim = int.Parse(Console.ReadLine()) - 1;

                if (ucakSecim < 0 || ucakSecim >= _ucaklar.Count)
                {
                    Console.WriteLine("Geçersiz uçak seçimi!");
                    return;
                }

                Ucus yeniUcus = new Ucus(ucusNo, kalkis, varis, tarih, saat, _ucaklar[ucakSecim], fiyat);
                _ucuslar.Add(yeniUcus);

                Console.WriteLine($"\n✓ Uçuş başarıyla eklendi: {yeniUcus.BilgiGoster()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: Geçersiz giriş! ({ex.Message})");
            }
        }

        // ==================== REZERVASYON İŞLEMLERİ ====================

        // Rezervasyon oluştur
        public void RezervasyonOlustur(Ucus ucus)
        {
            Console.WriteLine($"\n=== REZERVASYON: {ucus.UcusNo} ===");

            // Koltuk haritasını göster
            ucus.KoltukHaritasiGoster();

            // Koltuk seçimi
            Console.Write("\nKoltuk numarası girin (örn: 1A): ");
            string koltukNo = Console.ReadLine();

            Koltuk seciliKoltuk = ucus.KoltukBul(koltukNo);

            if (seciliKoltuk == null)
            {
                Console.WriteLine("Geçersiz koltuk numarası!");
                return;
            }

            if (seciliKoltuk.Dolu)
            {
                Console.WriteLine("Bu koltuk dolu! Lütfen başka bir koltuk seçin.");
                return;
            }

            // Yolcu bilgileri
            Console.WriteLine("\n--- Yolcu Bilgileri ---");

            Console.Write("TC Kimlik No: ");
            string tcNo = Console.ReadLine();

            Console.Write("Ad: ");
            string ad = Console.ReadLine();

            Console.Write("Soyad: ");
            string soyad = Console.ReadLine();

            Console.Write("Doğum Tarihi (GG.AA.YYYY): ");
            DateTime dogumTarihi;
            if (!DateTime.TryParse(Console.ReadLine(), out dogumTarihi))
            {
                Console.WriteLine("Geçersiz tarih formatı!");
                return;
            }

            Console.Write("Cinsiyet (E/K): ");
            string cinsiyet = Console.ReadLine().ToUpper() == "E" ? "Erkek" : "Kadın";

            // Yolcu oluştur
            Yolcu yolcu = new Yolcu(tcNo, ad, soyad, dogumTarihi, cinsiyet);

            // Rezervasyon oluştur
            Rezervasyon rezervasyon = new Rezervasyon(ucus, yolcu, seciliKoltuk);
            _rezervasyonlar.Add(rezervasyon);

            // Bileti yazdır
            Console.WriteLine("\n✓ Rezervasyon başarıyla oluşturuldu!");
            rezervasyon.BiletYazdir();
        }

        // PNR ile rezervasyon sorgula
        public Rezervasyon RezervasyonSorgula(string pnr)
        {
            foreach (var rez in _rezervasyonlar)
            {
                if (rez.PNR.ToUpper() == pnr.ToUpper())
                {
                    return rez;
                }
            }
            return null;
        }

        // Rezervasyon sorgulama menüsü
        public void RezervasyonSorgulaMenu()
        {
            Console.WriteLine("\n=== REZERVASYON SORGULA ===");
            Console.Write("PNR Kodu girin: ");
            string pnr = Console.ReadLine();

            Rezervasyon rez = RezervasyonSorgula(pnr);

            if (rez == null)
            {
                Console.WriteLine("Bu PNR koduna ait rezervasyon bulunamadı!");
                return;
            }

            rez.BiletYazdir();
        }

        // Rezervasyon iptal menüsü
        public void RezervasyonIptalMenu()
        {
            Console.WriteLine("\n=== REZERVASYON İPTAL ===");
            Console.Write("İptal edilecek PNR Kodu: ");
            string pnr = Console.ReadLine();

            Rezervasyon rez = RezervasyonSorgula(pnr);

            if (rez == null)
            {
                Console.WriteLine("Bu PNR koduna ait rezervasyon bulunamadı!");
                return;
            }

            if (rez.Durum == RezervasyonDurumu.IptalEdildi)
            {
                Console.WriteLine("Bu rezervasyon zaten iptal edilmiş!");
                return;
            }

            // Onay al
            Console.WriteLine($"\nİptal edilecek rezervasyon:");
            Console.WriteLine(rez.RezervasyonBilgisi());
            Console.Write("\nİptal etmek istediğinize emin misiniz? (E/H): ");

            if (Console.ReadLine().ToUpper() == "E")
            {
                rez.IptalEt();
                Console.WriteLine("\n✓ Rezervasyon başarıyla iptal edildi.");
            }
            else
            {
                Console.WriteLine("İptal işlemi vazgeçildi.");
            }
        }

        // Tüm rezervasyonları listele (Admin için)
        public void TumRezervasyonlariListele()
        {
            Console.WriteLine("\n" + new string('=', 80));
            Console.WriteLine("                      TÜM REZERVASYONLAR");
            Console.WriteLine(new string('=', 80));

            if (_rezervasyonlar.Count == 0)
            {
                Console.WriteLine("Sistemde kayıtlı rezervasyon bulunmamaktadır.");
                return;
            }

            foreach (var rez in _rezervasyonlar)
            {
                Console.WriteLine(rez.RezervasyonBilgisi());
            }
            Console.WriteLine(new string('=', 80));
        }

        // ==================== MENÜLER ====================

        // Ana menü
        public void AnaMenuGoster()
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("     UÇAK BİLETİ REZERVASYON SİSTEMİ");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("  1. Uçuş Ara");
            Console.WriteLine("  2. Tüm Uçuşları Listele");
            Console.WriteLine("  3. Rezervasyon Sorgula (PNR)");
            Console.WriteLine("  4. Rezervasyon İptal Et");
            Console.WriteLine("  5. Admin Paneli");
            Console.WriteLine("  0. Çıkış");
            Console.WriteLine(new string('=', 50));
            Console.Write("  Seçiminiz: ");
        }

        // Admin menüsü
        public void AdminMenuGoster()
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("           ADMİN PANELİ");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("  1. Yeni Uçuş Ekle");
            Console.WriteLine("  2. Tüm Uçuşları Listele");
            Console.WriteLine("  3. Tüm Rezervasyonları Listele");
            Console.WriteLine("  0. Ana Menüye Dön");
            Console.WriteLine(new string('=', 50));
            Console.Write("  Seçiminiz: ");
        }

        // Admin paneli işlemleri
        public void AdminPaneli()
        {
            bool devam = true;
            while (devam)
            {
                AdminMenuGoster();
                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        UcusEkle();
                        break;
                    case "2":
                        UcuslariListele();
                        break;
                    case "3":
                        TumRezervasyonlariListele();
                        break;
                    case "0":
                        devam = false;
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim!");
                        break;
                }
            }
        }

        // Ana döngü
        public void Calistir()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║     UÇAK BİLETİ REZERVASYON SİSTEMİNE HOŞ GELDİNİZ     ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");

            bool devam = true;
            while (devam)
            {
                AnaMenuGoster();
                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        UcusAraMenu();
                        break;
                    case "2":
                        UcuslariListele();
                        break;
                    case "3":
                        RezervasyonSorgulaMenu();
                        break;
                    case "4":
                        RezervasyonIptalMenu();
                        break;
                    case "5":
                        AdminPaneli();
                        break;
                    case "0":
                        devam = false;
                        Console.WriteLine("\nSistemden çıkılıyor. Güle güle!");
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim! Lütfen tekrar deneyin.");
                        break;
                }
            }
        }
    }
}