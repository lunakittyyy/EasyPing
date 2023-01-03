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

        // Request the CommandHandler
        // This gets resolved by zenject since we bind MyModCommandManager in the container
        public EasyPingCommand(CommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        public void Initialize()
        {
            _commandTokens = new List<CommandToken>();

            // Add a command
            // You can pass null in argumentsType if you aren't expecting any
            RegisterCommand(new Command(name: "ping", argumentTypes: null, args =>
            {
                // args is an array of arguments (string) passed when entering the command
                // the command handler already checks if the correct amount of arguments is passed

                // the string you return is going to be shown in the terminal as a return message
                // you can break up the message into multiple lines by using \n
                if (PhotonNetwork.InRoom == true)
                {
                    return "Roundtrip ping is " + PhotonNetwork.GetPing() + "ms";
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