hello_web_api
hello_web_api_client
這兩個project主要是做書上p.61後的範例
可參考:
https://hk.saowen.com/a/ff89f9c11217b516ff342f2b40b2ba1ba1f39caf12601f723c3dabb7a6f9d060
### hello_web_api
* 自己新增Conttrollers/GreetingController.cs , Models/Greeting.cs
* GreetingController.cs
    * PostGreeting()
        * a Location header pointing to the URL of the newly created greeting resource.

        * returns an HTTP 201 status code
        * HTTP response header "Location": 
        https://developer.mozilla.org/zh-CN/docs/Web/HTTP/Headers/Location
            * Location 首部指定的是需要将页面重新定向至的地址。一般在响应码为3xx的响应中才会有意义。
            * 除了重定向响应之外， 状态码为 201 (Created) 的消息也会带有Location首部。它指向的是新创建的资源的地址。
            * 201 Created
The request has succeeded and a new resource has been created as a result of it. This is typically the response sent after a PUT request.
            

    * GetGreeting()
        
### hello_web_api_client
* HTTP client 類別: https://msdn.microsoft.com/zhtw/library/system.net.http.httpclient(v=vs.110).aspx
* 利用GetAsync()送request

### Example
#### Post: (get the location)
![](https://i.imgur.com/U83G6kI.png)

#### Get: 
* ##### Use postman
    !![](https://i.imgur.com/aVKlHcv.png)

* ##### Use hello_web_api_client
    ![](https://i.imgur.com/W2z8QG0.png)
    
    ![](https://i.imgur.com/fEvKuDp.png)

