# Blockies For Unity

Welcome, this repository is a unity package that you can use in your Unity project to generate blockies.  

The code of   
- https://github.com/ethereum/blockies/blob/master/blockies.js  
Modified by scelts for C# with Form app  
- https://github.com/scelts/eth-blockies.net/blob/master/ETH_Identicons/Identicon.cs  
Adapted by me for beeing used in Unity  
- https://github.com/EloiStree/2021_05_29_BlockiesForUnity  

> I am not at all the creator of this code. I just modified it to make it usable in Unity project.  

## How to use

Git to clone https://github.com/EloiStree/2021_05_29_BlockiesForUnity.git  
![image](https://user-images.githubusercontent.com/20149493/120058549-f165e880-c04b-11eb-817e-06ef57d03672.png)  
![image](https://user-images.githubusercontent.com/20149493/120058540-df844580-c04b-11eb-8f5e-f158695b905a.png)  
![image](https://user-images.githubusercontent.com/20149493/120058561-09d60300-c04c-11eb-90d0-b9a3de3bb04e.png)  


## What is in the demo

You can use:  
``` csharp
Texture2D texture = Blockies.GetAsDefaultFor(address, blocksize, scale);
```
![image](https://user-images.githubusercontent.com/20149493/120058528-c085b380-c04b-11eb-9d42-3f504e904b49.png)



The code is just a file:   
https://github.com/EloiStree/2021_05_29_BlockiesForUnity/blob/main/Runtime/BlockyMono.cs  
_The rest is just to make it packagable._  
