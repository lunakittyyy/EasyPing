using System.Collections.Generic;
using ComputerInterface;
using Zenject;
using Photon.Pun;

namespace ComputerModExample
{
    internal class EasyPingCommand : IInitializable
    {
        private readonly CommandHandler _commandHandler;
        private List<CommandToken> _commandTokens;

        public EasyPingCommand(CommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        public void Initialize()
        {
            _commandTokens = new List<CommandToken>();

            RegisterCommand(new Command(name: "ping", argumentTypes: null, args =>
            {
                if (PhotonNetwork.InRoom == true)
                {
                    // thanks to Frogrilla for the region part
                    // https://github.com/Frogrilla/RCH/blob/75308bb0f5f8e587be251942738ad6cbc3d29e53/src/Manager.cs#L59
                    return "Roundtrip ping is " + PhotonNetwork.GetPing() + "ms in room of region " + PhotonNetwork.CloudRegion.Replace("/*", "").ToUpper();
                }
                else
                {
                    return "Not in a room";
                }
            }));

            void RegisterCommand(Command cmd)
            {
                var token = _commandHandler.AddCommand(cmd);
                _commandTokens.Add(token);
            }

            void UnregisterAllCommands()
            {
                foreach (var token in _commandTokens)
                {
                    token.UnregisterCommand();
                }
            }

            void Dispose()
            {
                UnregisterAllCommands();
            }
        }
    }
}