#!/bin/bash

cd Builds/Linux

echo "#############"
echo "# Archiving #"
echo "#############"
7z a SpaceDreams.linux.7z SpaceDreams

echo
echo "#############"
echo "# Uploading #"
echo "#############"
scp SpaceDreams.linux.7z bering@ringlogic.com:public_html/unity

cd -
