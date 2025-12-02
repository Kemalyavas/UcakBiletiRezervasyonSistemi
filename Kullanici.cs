using System;

namespace UcakBiletiRezervasyonSistemi
{
    // Abstract (soyut) kullanıcı sınıfı - OOP Abstraction prensibi
    public abstract class Kullanici
    {
        // Private field'lar - Encapsulation prensibi
        private int _kullaniciId;
        private string _ad;
        private string _soyad;
        private string _email;
        private string _telefon;

        // Public property'ler - Encapsulation
        public int KullaniciId
        {
            get { return _kullaniciId; }
            set { _kullaniciId = value; }
        }

        public string Ad
        {
            get { return _ad; }
            set { _ad = value; }
        }

        public string Soyad
        {
            get { return _soyad; }
            set { _soyad = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Telefon
        {
            get { return _telefon; }
            set { _telefon = value; }
        }

        // Constructor (Yapıcı metot)
        public Kullanici(int kullaniciId, string ad, string soyad, string email, string telefon)
        {
            _kullaniciId = kullaniciId;
            _ad = ad;
            _soyad = soyad;
            _email = email;
            _telefon = telefon;
        }

        // Abstract metot - Alt sınıflar tarafından override edilmek zorunda (Polymorphism)
        public abstract string BilgiGoster();

        // Virtual metot - Alt sınıflar tarafından override edilebilir
        public virtual string TamAd()
        {
            return $"{_ad} {_soyad}";
        }
    }
}