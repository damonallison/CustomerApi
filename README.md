# CustomerApi

[![Build Status](https://dev.azure.com/damon0269/customer-api/_apis/build/status/damonallison.CustomerApi?branchName=master)](https://dev.azure.com/damon0269/customer-api/_build/latest?definitionId=1&branchName=master)

An example ASP.NET Web API which demonstrates the following:

* JWT validation using Okta
* Azure environment: ACR / AKS / Azure Pipelines
* Automated docker / k8s deployment pipeline

This was initially built so I could get familiar with Azure, AKS, ACR, and using Azure Pipelines for CI/CD. I later added Okta / JWT support for auth and started hooking it up to a MongoDB instance.

## Installing MongoDB

```shell

# Tap the MongoDB formula repository
$ brew tap mongodb/brew

# Install
$ brew instalol mongodb-community

# Running (via a brew service)
$ brew services start mnogodb/brew/mongodb-community

# Run w/o background service
$ mongod --config /usr/local/etc/mongod.conf


$ mongo

> use CustomerDb
> db.createCollection('Customers')
> db.insertOne({'firstName': 'damon', 'lastName': 'allison'})

> db.Customers.find({}).pretty()

```
