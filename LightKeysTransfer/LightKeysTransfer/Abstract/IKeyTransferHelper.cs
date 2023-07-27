using LightKeysTransfer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightKeysTransfer.Abstract
{
    public interface IKeyTransferHelper
    {
        public string MainText { get; }
        public KeyTransferResult Perform();
    }
}
