using System;
using System.Collections.Generic;
using System.Linq;

namespace zettlers
{
    interface IZettler
    {
    }

    class ZettlersList
    {
        public ZettlersList()
        {
            foreach (var profession in _zettlers)
            {
                _zettlers[profession.Key] = new List<IZettler>();
            }
        }
        public IEnumerable<TZettler> GetZettlers<TZettler>() where TZettler : IZettler
        {
            return _zettlers[typeof(TZettler)].Cast<TZettler>();
        }

        private Dictionary<Type, List<IZettler>> _zettlers = 
            new Dictionary<Type, List<IZettler>>();

        internal void Add<TZettler>(TZettler zettler)
        {
            _zettlers[zettler.GetType()].Add((IZettler)zettler);
        }
    }
}