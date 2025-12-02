using System;

namespace UcakBiletiRezervasyonSistemi
{
    // Ana program - Giriş noktası
    class Program
    {
        static void Main(string[] args)
        {
            // Konsol ayarları (Türkçe karakter desteği)
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Rezervasyon sistemini başlat
            RezervasyonSistemi sistem = new RezervasyonSistemi();

            // Sistemi çalıştır
            sistem.Calistir();
        }
    }
}