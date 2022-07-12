using BelediyeProject.Entities;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BelediyeProject.Business
{
    public class RaporIslemBS
    {
        public const string IslemRaporQuery = @"
                                            --declare @IslemRaporBaslangic date=null;
                                            --declare @IslemRaporBitis date=null;
                                            --declare @IslemRaporDosyaAcilmaNedenTipKey int=null;
                                            --declare @IslemRaporDosyaDurumTipKey int=null;
                                            --declare @IslemRaporDosyaGonderilenBirimTipKey int=null;
                                            --declare @IslemRaporDosyaMesajTipKey int=null;
                                            --declare @IslemRaporMesaj varchar(500)=null;
                                            --declare @IslemRaporAciklama varchar(500)=null;
                                            --declare @IslemRaporAktifMi bit=1;

                                            select 
                                            DANT.DosyaAcilmaNedenTipAdi,
                                            DDT.DosyaDurumTipAdi,
                                            DGBT.DosyaGonderilecekBirimTipAdi,
                                            DMT.DosyaMesajTipAdi,
                                            D.Mesaj,
                                            D.Aciklama,
                                            D.IslemZamani,
                                            D.AktifMi
                                            from [dbo].[Dosya] as D
                                            inner join [dbo].[tt_DosyaAcilmaNedenTip] as DANT
                                            on DANT.DosyaAcilmaNedenTipKey=D.DosyaAcilmaNedenTipKey
                                            inner join [dbo].[tt_DosyaDurumTip] as DDT
                                            on DDT.DosyaDurumTipKey=D.DosyaDurumTipKey
                                            inner join [dbo].[tt_DosyaGonderilecekBirimTip] as DGBT
                                            on DGBT.DosyaGonderilecekBirimTipKey=D.DosyaGonderilecekBirimTipKey
                                            inner join [dbo].[tt_DosyaMesajTip] as DMT
                                            on DMT.DosyaMesajTipKey=D.DosyaMesajTipKey
                                            where 1=1
                                            and (D.IslemZamani between @IslemRaporBaslangic and @IslemRaporBitis)
                                            and (@IslemRaporDosyaAcilmaNedenTipKey =0 or @IslemRaporDosyaAcilmaNedenTipKey is null or @IslemRaporDosyaAcilmaNedenTipKey=D.DosyaAcilmaNedenTipKey)
                                            and (@IslemRaporDosyaDurumTipKey =0 or @IslemRaporDosyaDurumTipKey is null or @IslemRaporDosyaDurumTipKey=D.DosyaDurumTipKey)
                                            and (@IslemRaporDosyaGonderilenBirimTipKey =0 or @IslemRaporDosyaGonderilenBirimTipKey is null or @IslemRaporDosyaGonderilenBirimTipKey=D.DosyaGonderilecekBirimTipKey)
                                            and (@IslemRaporDosyaMesajTipKey =0 or @IslemRaporDosyaMesajTipKey is null or @IslemRaporDosyaMesajTipKey=D.DosyaMesajTipKey)
                                            and (@IslemRaporAktifMi=D.AktifMi)
                                            ";

        public static RaporIslemViewModel RaporGetir(ref RaporIslemViewModel raporIslemViewModel)
        {
            var kullaniciData = GirisIslemBS.KullaniciDataGetir();

            DateTime islemRaporBaslangic = raporIslemViewModel.IslemRaporBaslangic;
            DateTime islemRaporBitis = raporIslemViewModel.IslemRaporBitis;
            int islemRaporDosyaAcilmaNedenTipKey = raporIslemViewModel.IslemRaporDosyaAcilmaNedenTipKey;
            int islemRaporDosyaDurumTipKey = raporIslemViewModel.IslemRaporDosyaDurumTipKey;
            int islemRaporDosyaGonderilenBirimTipKey = raporIslemViewModel.IslemRaporDosyaGonderilenBirimTipKey;
            int islemRaporDosyaMesajTipKey = raporIslemViewModel.IslemRaporDosyaMesajTipKey;
            bool islemRaporAktifMi = raporIslemViewModel.IslemRaporAktifMi;
            string islemRaporMesaj = raporIslemViewModel.IslemRaporMesaj;
            string islemRaporAciklama = raporIslemViewModel.IslemRaporAciklama;

            using (DBEntities entities = new DBEntities())
            {
                var pIslemRaporBaslangic = new SqlParameter("@IslemRaporBaslangic", islemRaporBaslangic);
                var pIslemRaporBitis = new SqlParameter("@IslemRaporBitis", islemRaporBitis);
                var pIslemRaporDosyaAcilmaNedenTipKey = new SqlParameter("@IslemRaporDosyaAcilmaNedenTipKey", islemRaporDosyaAcilmaNedenTipKey);
                var pIslemRaporDosyaDurumTipKey = new SqlParameter("@IslemRaporDosyaDurumTipKey", islemRaporDosyaDurumTipKey);
                var pIslemRaporDosyaGonderilenBirimTipKey = new SqlParameter("@IslemRaporDosyaGonderilenBirimTipKey", islemRaporDosyaGonderilenBirimTipKey);
                var pIslemRaporDosyaMesajTipKey = new SqlParameter("@IslemRaporDosyaMesajTipKey", islemRaporDosyaMesajTipKey);
                var pIslemRaporAktifMi = new SqlParameter("@IslemRaporAktifMi", islemRaporAktifMi);

                //var parameters = new object[] { };
                var parameters = new object[]
                   {
                                pIslemRaporBaslangic,
                                pIslemRaporBitis,
                                pIslemRaporDosyaAcilmaNedenTipKey,
                                pIslemRaporDosyaDurumTipKey,
                                pIslemRaporDosyaGonderilenBirimTipKey,
                                pIslemRaporDosyaMesajTipKey,
                                pIslemRaporAktifMi,
                   };

                raporIslemViewModel.IslemRaporListeler = entities.Database.SqlQuery(typeof(IslemRaporData),
                                                                 IslemRaporQuery,
                                                                 parameters).
                                                                 Cast<IslemRaporData>().
                                                                 Where(p =>
                                                                (islemRaporMesaj == null || (islemRaporMesaj != null && p.Mesaj != null && p.Mesaj.Contains(islemRaporMesaj))) &&
                                                                 (islemRaporAciklama == null || (islemRaporAciklama != null && p.Aciklama != null && p.Aciklama.Contains(islemRaporAciklama)))).
                                                                 ToList();
            }

            return raporIslemViewModel;
        }

        public static List<tt_DosyaAcilmaNedenTip> DosyaAcilmaNedenTipList()
        {
            List<tt_DosyaAcilmaNedenTip> data = new List<tt_DosyaAcilmaNedenTip>();
            using (DBEntities entities = new DBEntities())
            {
                data = entities.tt_DosyaAcilmaNedenTip.
                                AsNoTracking().
                                Where(p => p.AktifMi).
                                ToList();
                data.Insert(0, new tt_DosyaAcilmaNedenTip
                {
                    DosyaAcilmaNedenTipKey = 0,
                    DosyaAcilmaNedenTipAdi = "Tümü"
                });
            }
            return data;
        }

        public static List<tt_DosyaDurumTip> DosyaDurumTipList()
        {
            List<tt_DosyaDurumTip> data = new List<tt_DosyaDurumTip>();
            using (DBEntities entities = new DBEntities())
            {
                data = entities.tt_DosyaDurumTip.
                               AsNoTracking().
                               Where(p => p.AktifMi).
                               ToList();
                data.Insert(0, new tt_DosyaDurumTip
                {
                    DosyaDurumTipKey = 0,
                    DosyaDurumTipAdi = "Tümü"
                });

            }
            return data;
        }

        public static List<tt_DosyaGonderilecekBirimTip> DosyaGonderilecekBirimTipList()
        {
            List<tt_DosyaGonderilecekBirimTip> data = new List<tt_DosyaGonderilecekBirimTip>();
            using (DBEntities entities = new DBEntities())
            {
                data = entities.tt_DosyaGonderilecekBirimTip.
                               AsNoTracking().
                               Where(p => p.AktifMi).
                               ToList();
                data.Insert(0, new tt_DosyaGonderilecekBirimTip
                {
                    DosyaGonderilecekBirimTipKey = 0,
                    DosyaGonderilecekBirimTipAdi = "Tümü"
                });

            }
            return data;
        }

        public static List<tt_DosyaMesajTip> DosyaMesajTipList()
        {
            List<tt_DosyaMesajTip> data = new List<tt_DosyaMesajTip>();
            using (DBEntities entities = new DBEntities())
            {
                data = entities.tt_DosyaMesajTip.
                               AsNoTracking().
                               Where(p => p.AktifMi).
                               ToList();
                data.Insert(0, new tt_DosyaMesajTip
                {
                    DosyaMesajTipKey = 0,
                    DosyaMesajTipAdi = "Tümü"
                });

            }
            return data;
        }
    }
}