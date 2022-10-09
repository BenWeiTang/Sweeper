# ScriptableObject-based Animation System

The system is an implementation of the Facade Pattern, interfacing DOTween while also incorporating async operations in native C#. It provides two types of animations. [Serialized Target Animation](https://github.com/BenWeiTang/Sweeper/blob/Main/Assets/Scripts/Animation/ASerializedTargetAnimation.cs) and [Dynamic Target Animation](https://github.com/BenWeiTang/Sweeper/blob/Main/Assets/Scripts/Animation/ADynamicTargetAnimation.cs).

The Serialized Target Animation presupposes the destination of a tween be provided by the user upon invocation. It is useful for moving UI elements as well as changing their colors, of which the target is usually configured in the Inspector.

The Dynamic Target Animation requires the destination to be passed in as an argument. It is useful when the destination is deteremined at runtime. An [example](https://github.com/BenWeiTang/Sweeper/blob/Main/Assets/Scripts/Animation/Grid/MoveAllTo.cs) is moving the blocks around during the intro animation of every game for the destination of each block is dynamically calculated. 
