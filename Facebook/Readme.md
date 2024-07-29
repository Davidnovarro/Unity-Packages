### ⚠️ Note: @davitmk added the PreservePackageXmLinkFile.cs file to ensure the preservation of the link.xml file.
This is necessary because Unity ignores link.xml files located within packages.

### ⚠️ Note: @davitmk added the EmbedDynamicLibsPostProcessIOS.cs file to ensure that iOS builds automatically embed dynamic libraries when "Link Frameworks Statically" is enabled in ExternalDependencyManager > iOS Settings.
This is necessary because Unity ignores link.xml files located within packages.

### Installation:
Open the Unity Package Manager and add a package from a Git URL. "https://github.com/Davidnovarro/Unity-Packages.git?path=/Facebook/17.0.1/package"