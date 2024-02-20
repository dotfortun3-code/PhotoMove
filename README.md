# PhotoMove
A simple application to organize your photos by date. It will organize your photos into a folder structure yyyy/mm/dd.

Please note, this has only been tested by me and does move files, use at your own risk! I recommend making a copy of the files you want to sort before running.

# Usage
Run the program from the terminal in the root of the folder you want to organize. You can pass a parameter to have it traverse all subfolders as well.

The following would sort the folders (and sub-folders) of C:\Pictures into C:\Pictures\yyyy\mm\dd:


```-m -R -p "C:\Pictures"```

You can use the following options to change the way the program functions:

```-p, -path (REQUIRED)``` The path the program will sort.

```-R, -recursive``` The program traverse all subfolders and sort all photos to the the parameter passed in the path (-p) option

```-r, -rename``` This will rename the files, by attempting to prepend the device IDs.

```-m, -move``` This option actually moves the files, in any other case, it will only output the changes that would occur.
