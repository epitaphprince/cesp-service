using System;

namespace CESP.Core.Models
{
    public enum BunchGroupEnum
    {
        Children = 1,
        Catalan = 3,
        Other = 0
    }

    public static class BunchGroupEnumConverter
    {
        public static BunchGroupEnum ParseBunchGroupEnum(int? bunchId)
        {
            if (bunchId == null)
                return (BunchGroupEnum)bunchId;
            
            if (!Enum.IsDefined(typeof(BunchGroupEnum), bunchId))
                return BunchGroupEnum.Other;

            return (BunchGroupEnum)bunchId;
        }
    }
}