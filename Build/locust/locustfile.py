import time
from locust import task, between
from locust.contrib.fasthttp import FastHttpUser

class QuickstartUser(FastHttpUser):

    @task
    def cluster_message(self):
        self.client.get("/Cluster/message?message=333")