using System;

namespace UcakBiletiRezervasyonSistemi
{
    // Koltuk tipi enum
    public enum KoltukTipi
    {
        Pencere,
        Orta,
        Koridor
    }

    // Koltuk sınıfı
    public class Koltuk
    {
        // Private field'lar
        private string _koltukNo;
        private bool _dolu;
        private KoltukTipi _tip;

        // Public property'ler
        public string KoltukNo
        {
            get { return _koltukNo; }
            set { _koltukNo = value; }
        }

        public bool Dolu
        {
            get { return _dolu; }
            set { _dolu = value; }
        }

        public KoltukTipi Tip
        {
            get { return _tip; }
            set { _tip = value; }
        }

        // Constructor
        public Koltuk(string koltukNo, KoltukTipi tip)
        {
            _koltukNo = koltukNo;
            _dolu = false; // Başlangıçta boş
            _tip = tip;
        }

        // Koltuğu rezerve et
        public bool Rezervasyon()
        {
            if (_dolu)
            {
                return false; // Zaten dolu
            }
            _dolu = true;
            return true;
        }

        // Koltuğu boşalt (iptal durumunda)
        public void Bosalt()
        {
            _dolu = false;
        }

        // Koltuk durumunu göster
        public string DurumGoster()
        {
            string durum = _dolu ? "DOLU" : "BOŞ";
            return $"[{_koltukNo}] {_tip} - {durum}";
        }
    }
}