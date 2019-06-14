using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZZULIWeb.Models
{
    public class Move : DataBase<ZZULIWeb.Move>
    {
        public int Insert(ZZULIWeb.Move move)
        {
            ZZULIWeb.Move mov = move;
            base.EF.Move.Add(mov);
            base.EF.SaveChanges();
            return mov.Mov_ID;
        }
    }
}