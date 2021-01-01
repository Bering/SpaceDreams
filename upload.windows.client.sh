#!/bin/bash

cd Builds/Windows

echo "#############"
echo "# Archiving #"
echo "#############"
zip -r SpaceDreams.windows.zip SpaceDreams

echo
echo "#############"
echo "# Uploading #"
echo "#############"
scp SpaceDreams.windows.zip bering@ringlogic.com:public_html/unity

cd -
