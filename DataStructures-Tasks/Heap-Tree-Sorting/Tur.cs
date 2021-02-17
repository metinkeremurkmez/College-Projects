using System;
using System.Collections.Generic;
using System.Text;

namespace Proje3B
{
    class Tur
    {
        public string turAdi;
        public int fiyat;
        public int gunSayisi;
        public int geceSayisi;
        public string[] geziRotasi;

    
        public Tur (string turAdi,int fiyat,int gunSayisi,int geceSayisi, string[] geziRotasi)
        {
            this.turAdi = turAdi;
            this.fiyat = fiyat;
            this.gunSayisi = gunSayisi;
            this.geceSayisi = geceSayisi;
            this.geziRotasi = geziRotasi;
            
        }

    }
}
