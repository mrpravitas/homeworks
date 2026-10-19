# homeworks

## Замер CPU (до / после оптимизации)

### До

| Метрика | Значение |
| --- | --- |
| `Update.ScriptRunBehaviourUpdate` | 19.2%, 0.14 ms |
| `Enemy.Update` | 107 calls, 0.04 ms |
| `Projectile.Update` | 74 calls, 0.04 ms |

### После

| Метрика | Значение |
| --- | --- |
| `Update.ScriptRunBehaviourUpdate` | 10.2%, 0.08 ms |
| `TickManager.Update` (вместо `Enemy.Update` + `Projectile.Update`) | 1 call, 0.06 ms |