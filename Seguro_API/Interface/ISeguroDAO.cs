using Seguro_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguro_API.Interface
{
    public interface ISeguroDAO
    {
        Seguro Select(int id);
        IEnumerable<Seguro> SelectAll();
        int Insert(Seguro seguro);
    }
}
