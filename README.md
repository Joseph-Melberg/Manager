# Welcome [![codecov](https://codecov.io/gh/Joseph-Melberg/Manager/branch/master/graph/badge.svg?token=Y27UDP57EP)](https://codecov.io/gh/Joseph-Melberg/Manager)

This repository contains the systems that I use to maintain my long term project: Inter.


# Purpose

I have always enjoyed constructing services and connecting them, so I figured that I would try to put together something that will keep me busy and entertained.

The second reason that I am doing this is to practice what I do in my day job.  The Onion Architecture, while simply the best long term architecture in terms of organization and maintainability, is not super straight forward, so this helps me understand the decisions behind where certain things go and what can view what.

# Components

## API's

### InterApi ![API CI-CD](https://github.com/Joseph-Melberg/Manager/workflows/API%20CI-CD/badge.svg)

Provides info for my website ( centurionx.net ) and for anyone else who wants it.

## Applications

### Heartbeat Listener ![Heartbeat CI-CD](https://github.com/Joseph-Melberg/Manager/workflows/.NET/badge.svg)

Receives messages from the nodes, giving me some insight into the current state of my work.

### LifeAlert

Runs every minute, checking the age of the heartbeats recorded.  If it finds a heartbeat that is both stale and unnanounced, it sends me an email.

### Log Listener

Listens on a rabbit queue to the logs that are sent to it.

### Plane Listener WIP

Will take the place of dump1090 as main data ingress point.  Will take in data on a port and process into plane data.  It will store that info in Redis in the following fashion.

Plane changes come in the form of updates to a single aspect of a given plane, so redis will host a collection of objects named $id_$key, with two values, $objectValue and $entry time.

A message will pull all of the aspects of the relevant plane, update the value and timestamp, then, if all of the ages are proper, push that plane record into the MySQL db.





# Notes

It looks like there are two people working on this product, but they are both me.

