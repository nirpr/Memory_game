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
        private bool m_IsVisible;
        private int m_RowNumber;
        private int m_ColNumber;

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
        public bool IsVisible
        {
            get
            {
                return m_IsVisible;
            }
            internal set
            {
                m_IsVisible = value;
            }
        }
        public int RowNumber
        {
            get
            {
                return m_RowNumber;
            }
            internal set
            {
                m_RowNumber = value;
            }
        }
        public int ColNumber
        {
            get
            {
                return m_ColNumber;
            }
            internal set
            {
                m_ColNumber = value;
            }
        }

        public void InitChosenLetter(T i_Value, int i_RowNumber, int i_colNumber)
        {
            this.CardValue = i_Value;
            this.RowNumber = i_RowNumber;
            this.ColNumber = i_colNumber;
        }
    }
}
