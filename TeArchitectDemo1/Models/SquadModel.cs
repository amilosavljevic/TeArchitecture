using System.Collections.Generic;
using TeArchitecture.Domain;
using TeArchitecture.Shared;
using TeArchitecture.Shared.MVC;

namespace TeArchitecture.Demo1
{
    public class SquadMode : Model<Squad>
    {
        public SquadMode(Squad data, IBus bus) : base(data, bus)
        {
        }

        protected override void OnInit()
        {
            base.OnInit();
        }
    }
}
