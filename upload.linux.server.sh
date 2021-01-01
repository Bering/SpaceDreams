#!/bin/bash

pid=`ssh father pgrep SpaceDreams`
ssh father kill $pid

ssh father rm -rf ~/SpaceDreams

scp -r Builds/Linux/Server father:SpaceDreams/
