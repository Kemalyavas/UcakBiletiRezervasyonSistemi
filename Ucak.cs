using System;

namespace UcakBiletiRezervasyonSistemi
{
    // Uçak sınıfı
    public class Ucak
    {
        // Private field'lar
        private int _ucakId;
        private string _ucakModeli;
        private int _kapasite;
        private string _koltukDuzeni; // Örn: "3-3" (koridor her iki tarafta 3'er koltuk)

        // Public property'ler
        public int UcakId
        {
            get { return _ucakId; }
            set { _ucakId = value; }
        }

        public string UcakModeli
        {
            get { return _ucakModeli; }
            set { _ucakModeli = value; }
        }

        public int Kapasite
        {
            get { return _kapasite; }
            set { _kapasite = value; }
        }

        public string KoltukDuzeni
        {
            get { return _koltukDuzeni; }
            set { _koltukDuzeni = value; }
        }

        // Constructor
        public Ucak(int ucakId, string ucakModeli, int kapasite, string koltukDuzeni)
        {
            _ucakId = ucakId;
            _ucakModeli = ucakModeli;
            _kapasite = kapasite;
            _koltukDuzeni = koltukDuzeni;
        }

        // Uçak bilgilerini göster
        public string BilgiGoster()
        {
            return $"Uçak: {_ucakModeli} | Kapasite: {_kapasite} | Düzen: {_koltukDuzeni}";
        }
    }
}