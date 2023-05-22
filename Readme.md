# Assetmanager V2
This is a rewrite of one of my old projects "assetmanager" done as a Semesterproject at my School.
## Softwaredesign
This app is split into three parts
1. A API
1. A Webapp
1. A standalone desktop App

Both the API and Webapp use the ASP.NET framework and the standalone app uses WPF.

## Description
This software was written for the intent purpose of storing static files in a central place, it has been written in a way to allow files to embed on other services.

This software is not intended to be:
1. A selfhosted cloud
2. Modifieable self storage

For a more detailed description of each of the components check the readme files inside of the subfolders.

## Discussion
This section will discuss issues and interesting events which have occured during development.

### Issues
#### WPF bindings:

This project has on several occasions been stuck without any progress for a long while as a WPF binding has inexplicably decided to stop working. My favorite example of this was where after compilation if you hot-reloaded a file it would suddenly work ~~sometimes~~.

#### WPF styling:

Thanks to the very interesting way of styling anything more complicated than changing the color of an element I was not able to get the WPF level to the level of Polish I wanted, nontheless I am still very happy with how it turned out.