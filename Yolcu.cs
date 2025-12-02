using System;

namespace UcakBiletiRezervasyonSistemi
{
    // Yolcu sınıfı
    public class Yolcu
    {
        // Private field'lar
        private string _tcNo;
        private string _ad;
        private string _soyad;
        private DateTime _dogumTarihi;
        private string _cinsiyet;

        // Public property'ler
        public string TcNo
        {
            get { return _tcNo; }
            set { _tcNo = value; }
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

        public DateTime DogumTarihi
        {
            get { return _dogumTarihi; }
            set { _dogumTarihi = value; }
        }

        public string Cinsiyet
        {
            get { return _cinsiyet; }
            set { _cinsiyet = value; }
        }

        // Constructor
        public Yolcu(string tcNo, string ad, string soyad, DateTime dogumTarihi, string cinsiyet)
        {
            _tcNo = tcNo;
            _ad = ad;
            _soyad = soyad;
            _dogumTarihi = dogumTarihi;
            _cinsiyet = cinsiyet;
        }

        // Yolcu bilgilerini göster
        public string BilgiGoster()
        {
            return $"{_ad} {_soyad} | TC: {_tcNo} | Doğum: {_dogumTarihi:dd.MM.yyyy} | {_cinsiyet}";
        }
    }
}