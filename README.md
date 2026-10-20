# homeworks

## Замер GC Alloc (до / после оптимизации)


| Проблема                                        | Файл                              | GC Alloc до | Исправление                   | GC Alloc после |
|---------------------------------------------------------|---------------------------------------|---------------|------------------------------------------|---------------------|
| Instantiate + Destroy для врагов               | `EnemySpawner.cs`, `Enemy.cs`         | 248B          | object pool                              | 0B                  |
| Instantiate + Destroy для проджектайлов | `Shooter.cs`, `ProjectileBoundary.cs` | 24B           | object pool                              | 0B                  |
| Строки                                            | `ScoreUI.cs`                          | 100B          | `ToString()` → `SetText("{0}", score)` | 0B                  |


 
