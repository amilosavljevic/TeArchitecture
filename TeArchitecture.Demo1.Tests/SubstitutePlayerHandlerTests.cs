using System.Linq;
using NUnit.Framework;
using TeArchitecture.Domain;
using TeArchitecture.Shared.Mock;

namespace TeArchitecture.Demo1.Tests
{
    public class SubstitutePlayerHandlerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestHappyPath()
        {
            var squad = GenerateSquad();
            var bus = new BusMock();
            var channel = new ChannelMock();

            var handler = new SubstitutePlayerHandler(bus, squad, channel);
            //handler.Process();
        }

        private Squad GenerateSquad()
        {
            return new Squad()
            {
                AllPlayers = Enumerable.Range(1, 20)
                            .Select(id => new PlayerData()
                            {
                                Id = new PlayerId(id),
                                Name = "Player " + id,
                                Age = 18 + id % 18,
                                Condition = new Condition(100),
                                Moral = new Moral(10),                                
                            }).ToList(),
                PlayersOnPitch = Enumerable.Range(1, 11)
                                .Select(id=> new PlayerId(id))
                                .ToList(),
            };
        }
    }
}