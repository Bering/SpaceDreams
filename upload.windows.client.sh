#!/bin/bash

cd Builds/Windows

echo "#############"
echo "# Archiving #"
echo "#############"
7z a SpaceDreams.windows.7z SpaceDreams

echo
echo "#############"
echo "# Uploading #"
echo "#############"
scp SpaceDreams.windows.7z bering@ringlogic.com:public_html/unity

cd -
