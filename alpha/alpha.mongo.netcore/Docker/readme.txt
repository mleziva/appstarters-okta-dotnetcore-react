Run docker-compose up in this folder to create the search, the mongodb, setup mongo db replication and create the connector between elastic search and db

If you don't want to use the compose, you can use the following manual commands
//running elastic DB
docker run -p 9200:9200 -p 9300:9300 -e "discovery.type=single-node" docker.elastic.co/elasticsearch/elasticsearch:7.2.0
http://127.0.0.1:9200/
http://localhost:9200/firstdb.test/_search?q=*&pretty

//running connector
docker run --net my-mongo-cluster rwynn/monstache -mongo-url mongodb://host.docker.internal:30001 -elasticsearch-url http://host.docker.internal:9200


//creating mongo replica set
docker network create my-mongo-cluster
docker run -p 30001:27017 --name mongo1 --net my-mongo-cluster mongo mongod --replSet my-mongo-set
docker run -d -p 30002:27017 --name mongo2 --net my-mongo-cluster mongo mongod --replSet my-mongo-set
docker run -d -p 30003:27017 --name mongo3 --net my-mongo-cluster mongo mongod --replSet my-mongo-set
docker exec -it mongo1 mongo
rsconf = {
  _id: "my-mongo-set",
  members: [
    {
     _id: 0,
     host: "mongo1:27017"
    },
    {
     _id: 1,
     host: "mongo2:27017"
    },
    {
     _id: 2,
     host: "mongo3:27017"
    }
   ]
}
rs.initiate(rsconf)