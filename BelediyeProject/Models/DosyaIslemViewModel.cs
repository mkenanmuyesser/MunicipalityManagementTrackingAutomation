using BelediyeProject.Business;
using BelediyeProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BelediyeProject.Models
{
    public partial class DosyaIslemViewModel : Dosya
    {
        public List<Dosya> GelenDosyalar { get; set; }
        public List<Dosya> IslemYapilanDosyalar { get; set; }
        public int? Id { get; set; }
        public DateTime BaslangicGelen { get; set; }
        public DateTime BitisGelen { get; set; }
        public DateTime BaslangicIslemYapilmis { get; set; }
        public DateTime BitisIslemYapilmis { get; set; }
        public string Bildirim { get; set; }

        public List<MenuData> Menu
        {
            get
            {
                return AnaSayfaBS.MenuOlustur();
            }
        }

        public List<tt_DosyaDurumTip> DosyaDurumTip
        {
            get
            {
                List<tt_DosyaDurumTip> tip = null;
                using (DBEntities entities = new DBEntities())
                {
                    tip = entities.tt_DosyaDurumTip.ToList();
                }
                return tip;
            }
        }

        public List<tt_DosyaGonderilecekBirimTip> GonderilecekBirimTip
        {
            get
            {
                List<tt_DosyaGonderilecekBirimTip> tip = null;
                using (DBEntities entities = new DBEntities())
                {
                    tip = entities.tt_DosyaGonderilecekBirimTip.ToList();
                }
                return tip;
            }
        }

        public List<tt_DosyaMesajTip> MesajTip
        {
            get
            {
                List<tt_DosyaMesajTip> tip = null;
                using (DBEntities entities = new DBEntities())
                {
                    tip = entities.tt_DosyaMesajTip.ToList();
                }
                return tip;
            }
        }

        public bool ResimHashDogrulama(int dosyaResimKey)
        {
            return DosyaIslemBS.ResimHashBilgiDogrulama(dosyaResimKey);
        }
    
    }
}