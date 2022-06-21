using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.DTO
{
    public class DownloadResponse : BaseResponse
    {
        public byte[] fileData { set; get; }
        public byte[] FileData { get; set; }
        public string Name { get; set; }
    }
}
