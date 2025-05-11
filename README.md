# 🎮 Top-Down Shooter Showcase Project  
**A demo highlighting design patterns, architecture, and Unity best practices**  

[![Unity Version](https://img.shields.io/badge/Unity-2022.3+-black.svg?logo=unity)](https://unity.com)
[![Design Patterns](https://img.shields.io/badge/Design%20Patterns-7%20implemented-blueviolet)](https://refactoring.guru/design-patterns)

<div align="center">
  <img src="https://via.placeholder.com/800x400.png?text=Gameplay+Screenshot" width="600" alt="Gameplay Preview">
</div>

## 🚀 Overview
A top-down shooter game developed as a technical showcase for a job interview. Focuses on:
- **Clean architecture** through design patterns
- **Scalable systems** for gameplay mechanics
- **Modular code** for easy maintenance
- **Performance optimizations**

## 🔧 Implemented Design Patterns
Here are key patterns demonstrated in this project:

### 1. **Factory Method** (Enemy Spawning System)
```csharp
public abstract class EnemySpawner : MonoBehaviour {
    public abstract Enemy CreateEnemy();
}

public class ZombieSpawner : EnemySpawner {
    public override Enemy CreateEnemy() {
        return Instantiate(zombiePrefab);
    }
}

public interface IWeaponFactory {
    IProjectile CreateProjectile();
    ISound CreateFireSound();
}

public class LaserWeaponFactory : IWeaponFactory {
    public IProjectile CreateProjectile() => new LaserBeam();
    public ISound CreateFireSound() => new LaserSound();
}

public class GameManager : MonoBehaviour {
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }
}

public class PlayerHealth : MonoBehaviour {
    public event Action OnDeath;
    
    private void Die() {
        OnDeath?.Invoke();
    }
}

public interface IPlayerState {
    void HandleInput(Player player);
}

public class RunningState : IPlayerState {
    public void HandleInput(Player player) {
        if (Input.GetKeyDown(KeyCode.Space)) {
            player.SetState(new JumpingState());
        }
    }
}

