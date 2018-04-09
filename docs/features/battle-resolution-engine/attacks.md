## Attack Trigger's [Video Explanation](https://www.youtube.com/watch?v=YyinHRy9Qtw)
Attack effects are invoked in two stages, Pre Damage & Post Damage. There is also a set of functions that can be invoked on the target of the attack or any other pokemon that will be invoked at the same time but are not in the context that a move is being executed... For example an ability that raises the Attack CS by one when an alli KO's another enemy pokemon. They also follow the same stages Pre Damage & Post Damage
#### Difference between Pre Damage & Post Damage
When an attack is made battle resolution engine (Battle Manager) will do some setup then invoke the `Attack_Pre_Invoked` function, This usually contains effects that are required to make changes to the the formula for example throwing items may effect the DB or AC of a move that throws items, the `Attack_Pre_Invoked` may also contain conditions that may cause the effect to cancel the move entirly...

The other function is `Attack_Post_Invoked` that invokes right after the damage has been applied to the target, Things like applying the Status Afflictions like Burn or Paralyses should be done here because they are always done at the end of an attack.

This is also applied the same to `SelfAttacked_Pre_Invoked` & `SelfAttacked_Post_Invoked` functions except these are invoked because the user with this script applied (Usually because of an ability) has been attacked. `AttackExecuted_Pre_Invoked` & `AttackExecuted_Post_Invoked` are invoked whenever an attack has been made, reguardless of the target

The table below has a list of functions, what order they get invoked and a description on when they are called
Order|      Function Called        |             Invoked When            |
-----|-----------------------------|--------------------------------------
  1  |[`Attack_Pre_Invoked`](http://www.virtual-ptu.com)|User Attacks, Before Damage Applied
  2  |[`SelfAttacked_Pre_Invoked`](http://www.virtual-ptu.com)|User Targeted, Before Damage Applied
  3  |[`AttackExecuted_Pre_Invoked`](http://www.virtual-ptu.com)|Attack Executed, Before Damage Applied
  -|  Execution Terminates Here    |If `Cancel` flag is set
  4  |[`Attack_Post_Invoked`](http://www.virtual-ptu.com)|User Attacks, After Damage Applied
  5  |[`SelfAttacked_Post_Invoked`](http://www.virtual-ptu.com)|User Targeted, After Damage Applied
  6  |[`AttackExecuted_Post_Invoked`](http://www.virtual-ptu.com)|Attack Executed, After Damage Applied