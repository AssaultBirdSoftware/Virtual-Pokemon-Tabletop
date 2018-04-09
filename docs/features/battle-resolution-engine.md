# Effect Scripts [Video Explanation](https://www.youtube.com/watch?v=YyinHRy9Qtw)

## Scripting Interface
The scripting interface has access to lots of different variables, and functions built in. So what are the different variables and functions that can be called upon to do the things you need? Firstly you need to know how the scripts are used inside the program...

Firstly the scripts are never loaded into memory untill they are needed. Secondly the scripting interface is event driven so making a script and puting the code in the root will break things. You will need to put it into a specific function depending on when you want to invoke it, the functions have specific names and are invoked at different times for different things e.g. if you put the `Attack_Pre_Invoked` function into your script, the contents of that function will be executed before the damage is applied to the target of the attack.
#### Functions
This is a list of all triggers that are present in the software usable in a script. Click the name to go to a page with more details about that trigger
 Type  |Group|        Trigger Name         | Invoked When
-------|-----|-----------------------------|-------------
Trigger|[Attack](http://www.virtual-ptu.com)|[`Attack_Pre_Invoked`](https://www.virtual-ptu.com)|User Attacks, Before Damage Applied
Trigger|[Attack](http://www.virtual-ptu.com)|[`Attack_Post_Invoked`](https://www.virtual-ptu.com)|User Attacks, After Damage Applied
Trigger|[Attack](http://www.virtual-ptu.com)|[`Attacked_Pre_Invoked`](https://www.virtual-ptu.com)|User Targeted, Before Damage Applied
Trigger|[Attack](http://www.virtual-ptu.com)|[`Attacked_Post_Invoked`](https://www.virtual-ptu.com)|User Targeted, After Damage Applied
Trigger|[Attack](http://www.virtual-ptu.com)|[`AttackExecuted_Pre_Invoked`](https://www.virtual-ptu.com)|Attack Executed, Before Damage Applied
Trigger|[Attack](http://www.virtual-ptu.com)|[`AttackExecuted_Post_Invoked`](https://www.virtual-ptu.com)|Attack Executed, After Damage Applied
Trigger|[Entity](http://www.virtual-ptu.com)|[`Entity_KO`](https://www.virtual-ptu.com)|Entity has fainted