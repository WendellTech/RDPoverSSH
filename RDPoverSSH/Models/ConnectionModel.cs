﻿using LiteDB;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace RDPoverSSH.Models
{
    public class ConnectionModel : ObservableObject
    {
        [BsonId]
        public int ObjectId { get; set; }

        public string Name { get; set; }

        public Direction ConnectionDirection
        {
            get => _connectionDirection;
            set => SetProperty(ref _connectionDirection, value);
        }
        private Direction _connectionDirection;

        public Direction TunnelDirection
        {
            get => _tunnelDirection;
            set => SetProperty(ref _tunnelDirection, value);
        }
        private Direction _tunnelDirection;
    }

    /// <summary>
    /// Describes possible connections directions, including the tunnel
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// From this computer to the remote computer
        /// </summary>
        Normal,

        /// <summary>
        /// From the remote computer to this computer
        /// </summary>
        Reverse
    }

    /// <summary>
    /// Extensions on <see cref="Direction"/>.
    /// </summary>
    public static class DirectionExtensions
    {
        /// <summary>
        /// This method toggles the value of the <paramref name="direction"/> to the opposite direction.
        /// </summary>
        /// <remarks>
        /// This both changes the reference of the given parameter, as well as returning the new value,
        /// so that it can be used in places where the existing value can or cannot be used by ref.
        /// </remarks>
        public static Direction Toggle(ref this Direction direction)
        {
            direction = direction switch
            {
                Direction.Normal => Direction.Reverse,
                Direction.Reverse => Direction.Normal,
                _ => Direction.Normal
            };

            return direction;
        }
    }
}
