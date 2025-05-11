using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Type-safe enum pattern implementation for enemy types
    /// Combines identification and configuration in a single type
    /// </summary>
    public sealed class EnemyType
    {
        // Predefined instances act as enum values
        public static readonly EnemyType Green = new(
            name: "Green",
            damage: 5,
            health: 3,
            color: Color.green,
            new StraightMovement(0.3f)
        );

        public static readonly EnemyType Yellow = new(
            name: "Yellow",
            damage: 5,
            health: 1,
            color: Color.yellow,
            new StraightMovement(0.45f)
        );

        public static readonly EnemyType Red = new(
            name: "Red",
            damage: 7,
            health: 5,
            color: Color.red,
            new StraightMovement(0.2f)
        );

        // Instance properties
        public string Name { get; }
        public int Damage { get; }
        public int Health { get; }
        public Color Color { get; }

        public IEnemyMovement MovementStrategy { get; }

        // Private constructor ensures controlled instance creation
        private EnemyType(string name, int damage, int health, Color color, IEnemyMovement movementStrategy)
        {
            Name = name;
            Damage = damage;
            Health = health;
            Color = color;
            MovementStrategy = movementStrategy;
        }
    }
}