using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorantScraper
{
    internal class AccountTemplateData
    {
        public string? Level { get; set; }
        public string? SkinsCount { get; set; }
        public string? Region { get; set; }
        public string? Screenshots { get; set; }
        public string? Rank { get; set; }
        public string? Tier { get; set; }
        public string? Episode { get; set; }
        public string? Act { get; set; }
        public string? Buddies { get; set; }
        public string? BuddiesSection => !string.IsNullOrEmpty(Buddies) ? $"=\n{Buddies}\n=" : "=";
        public string? AgentsCount { get; set; }
        public string? RPoints { get; set; }
        public string? VPoints { get; set; }
        public string? TotalVPoints { get; set; }
        public string? StartPrice { get; set; }
        public string? Price { get; set; }
        public string? SkinsList { get; set; }
        public string? SkinsSection { get; set; }
        public string? KnifeSkin { get; set; }
        public string? Skin1 { get; set; }
        public string? Skin2 { get; set; }
        public string? Skin3 { get; set; }
        public string? Skin4 { get; set; }
    }
}
