using System;
using System.Collections.Generic;

namespace Course.DAL.Models
{
    public class Language : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public ICollection<AudioLanguage> AudioLanguages { get; set; }
        public ICollection<CloseCaption> CloseCaptions { get; set; }
    }
}
