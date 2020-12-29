#!/bin/bash

pid=`ssh father pgrep SpaceDreams.Server`
ssh father kill $pid

ssh father rm -rf ~/SpaceDreams

scp -r Builds/Linux/Server father:SpaceDreams/
