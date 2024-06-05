using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameLogics
{
    public class PlayingCards<T>
    {
        private T m_CardValue;
        private eVisibleOptions m_VisibilityOption;

        public PlayingCards(T i_CardValue)
        {
            m_CardValue = i_CardValue;
            m_VisibilityOption = eVisibleOptions.NotVisible;
        }

        public T CardValue
        {
            get
            {
                return m_CardValue;
            }
            internal set
            {
                m_CardValue = value;
            }
        }
        public eVisibleOptions VisibilityOption
        {
            get
            {
                return m_VisibilityOption;
            }
            internal set
            {
                m_VisibilityOption = value;
            }
        }
    }
}
