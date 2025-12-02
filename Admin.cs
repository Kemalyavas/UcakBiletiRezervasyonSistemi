using System;

namespace UcakBiletiRezervasyonSistemi
{
    // Admin sınıfı - Kullanici'dan türetildi (Inheritance)
    public class Admin : Kullanici
    {
        // Private field
        private int _yetkiSeviyesi;

        // Public property
        public int YetkiSeviyesi
        {
            get { return _yetkiSeviyesi; }
            set { _yetkiSeviyesi = value; }
        }

        // Constructor
        public Admin(int kullaniciId, string ad, string soyad, string email, string telefon, int yetkiSeviyesi)
            : base(kullaniciId, ad, soyad, email, telefon)
        {
            _yetkiSeviyesi = yetkiSeviyesi;
        }

        // Override metot - Polymorphism prensibi
        public override string BilgiGoster()
        {
            return $"[ADMİN] {TamAd()} | Yetki Seviyesi: {_yetkiSeviyesi} | Email: {Email}";
        }
    }
}