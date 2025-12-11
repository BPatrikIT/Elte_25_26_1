using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GyorsHir.ViewModel
{
    public interface IShare
    {
        Task ShareUri(string uri, string title);
    }
}
