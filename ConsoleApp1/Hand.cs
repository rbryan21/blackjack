using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Hand
    {
        private List<Cards> faceUp;
        private List<Cards> faceDown;

        public Hand()
        {

        }

        public Hand(List<Cards> faceUp, List<Cards> faceDown)
        {
            this.faceUp = faceUp;
            this.faceDown = faceDown;
        }

        public List<Cards> FaceUp
        {
            get
            {
                return faceUp;
            }
        }

        public List<Cards> FaceDown
        {
            get
            {
                return faceDown;
            }
        }

        public void SetFaceDown(List<Cards> faceDown)
        {
            this.faceDown = faceDown;
        }

        public void SetFaceUp(List<Cards> faceUp)
        {
            this.faceUp = faceUp;
        }

        public void AddFaceUp(Cards card)
        {
            FaceUp.Add(card);
        }

        public void AddFaceDown(Cards card)
        {
            FaceDown.Add(card);
        }

        public int GetTotalValue()
        {
            int value = 0;
            int max = 21;

            if (FaceUp != null)
            {
                foreach (Cards card in FaceUp)
                {
                    switch (card.Value)
                    {
                        case 1:
                            if (value + 11 > max)
                            {
                                value += 1;
                            }
                            else
                            {
                                value += 11;
                            }
                            break;
                        case 11:
                        case 12:
                        case 13:
                            value += 10;
                            break;
                        default:
                            value += card.Value;
                            break;
                    }
                }
            }

            if (FaceDown != null)
            {
                foreach (Cards card in FaceDown)
                {
                    switch (card.Value)
                    {
                        case 1:
                            if (value + 11 > max)
                            {
                                value += 1;
                            }
                            else
                            {
                                value += 11;
                            }
                            break;
                        case 11:
                        case 12:
                        case 13:
                            value += 10;
                            break;
                        default:
                            value += card.Value;
                            break;
                    }
                }
            }
            return value;
        }
    }
}
