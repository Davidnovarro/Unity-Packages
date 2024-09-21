### ⚠️ Note: @davitmk modified the "Apple\core\package\Editor\AppleBuild.cs" file and added escape characters to the generated shell scripts.
This modification is necessary because those scripts will throw exceptions during the XCode build if the project path contains a space.
Here is a commit that should fix the same issue: https://github.com/apple/unityplugins/pull/34/commits/2ebaa716b3f02e208ec564097d35355effc1f2a3


## Build Instructions
* https://github.com/apple/unityplugins/tree/main
* https://github.com/apple/unityplugins/blob/main/Documentation/Quickstart.md
* https://github.com/apple/unityplugins/blob/main/Documentation/BuildScript.md


### Installation:
Open the Unity Package Manager and add the packages from a Git URL. 
* "https://github.com/Davidnovarro/Unity-Packages.git?path=/Apple/core/package"
* "https://github.com/Davidnovarro/Unity-Packages.git?path=/Apple/gamekit/package"