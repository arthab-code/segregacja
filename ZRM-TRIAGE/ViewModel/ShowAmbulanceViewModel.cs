using System;
using System.Collections.Generic;
using System.Text;

namespace ZRM_TRIAGE
{
    internal class ShowAmbulanceViewModel
    {
        public int IndexPicker(int index)
        {
            switch (index)
            {
                case 0:
                    return 0;
                    break;

                case 1:
                    return 1;
                    break;

                case 2:
                    return 2;
                    break;

                case 3:
                    return 3;
                    break;
                case 4:
                    return 4;
                    break;

                default:
                    return 4;
                    break;
            }
        }
    }
}
