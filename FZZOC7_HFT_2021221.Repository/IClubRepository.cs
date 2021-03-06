using FZZOC7_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FZZOC7_HFT_2021221.Repository
{
    public interface IClubRepository
    {
        void Create(Club club);
        Club Read(int id);
        IQueryable<Club> ReadAll();
        void Update(Club club);
        void Delete(int id);
    }
}
