using System;
using System.Collections.Generic;

namespace UcakBiletiRezervasyonSistemi
{
    // Musteri sınıfı - Kullanici'dan türetildi (Inheritance)
    public class Musteri : Kullanici
    {
        // Private field
        private string _tcNo;
        private List<Rezervasyon> _rezervasyonlar;

        // Public property
        public string TcNo
        {
            get { return _tcNo; }
            set { _tcNo = value; }
        }

        public List<Rezervasyon> Rezervasyonlar
        {
            get { return _rezervasyonlar; }
            set { _rezervasyonlar = value; }
        }

        // Constructor
        public Musteri(int kullaniciId, string ad, string soyad, string email, string telefon, string tcNo)
            : base(kullaniciId, ad, soyad, email, telefon)
        {
            _tcNo = tcNo;
            _rezervasyonlar = new List<Rezervasyon>();
        }

        // Override metot - Polymorphism prensibi
        public override string BilgiGoster()
        {
            return $"[MÜŞTERİ] {TamAd()} | TC: {_tcNo} | Email: {Email} | Tel: {Telefon}";
        }

        // Müşterinin rezervasyonlarını listele
        public void RezervasyonlariListele()
        {
            if (_rezervasyonlar.Count == 0)
            {
                Console.WriteLine("Henüz rezervasyonunuz bulunmamaktadır.");
                return;
            }

            Console.WriteLine($"\n{TamAd()} - Rezervasyonlarınız:");
            Console.WriteLine(new string('-', 50));
            foreach (var rez in _rezervasyonlar)
            {
                Console.WriteLine(rez.RezervasyonBilgisi());
            }
        }
    }
}