using BelediyeProject.Entities;
using BelediyeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BelediyeProject.Business
{
    public class ProgramIslemLogBS
    {
        public static ProgramIslemLogViewModel ProgramLogGetir()
        {
            ProgramIslemLogViewModel programIslemLogViewModel = new ProgramIslemLogViewModel();
            using (DBEntities entities = new DBEntities())
            {
                List<ProgramLog> programLogList = entities.ProgramLogs.
                                                    AsNoTracking().
                                                    ToList().
                                                    OrderByDescending(p=> p.Tarih).
                                                    Take(50).
                                                    ToList();
                programIslemLogViewModel.ProgramLogList = programLogList;              
            }

            return programIslemLogViewModel;
        }
    }
}