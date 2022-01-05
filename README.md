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

### Plane Listener

Listens for rabbitmq messages detailing the planes in the sky, uploads that info to redis for it to be accessed by the InterApi.

### Temperature Listener

Listens for rabbitmq messages detailing the temperature of some of components in my tech stack, including the Pi's and the servers I run my services on.

# Notes

It looks like there are two people working on this product, but they are both me.

