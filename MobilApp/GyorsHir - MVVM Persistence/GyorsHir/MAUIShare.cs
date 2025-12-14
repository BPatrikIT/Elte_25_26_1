using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GyorsHir
{
    public class MAUIShare : GyorsHir.ViewModel.IShare
    {
        public async Task ShareUri(string uri, string title)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = title
            });
        }
    }
}
