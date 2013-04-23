grendo-servicestack-plugins
===========================

Servicestack Plugins

I needed to be able to get json/xml from sericestack restful services in a pretty way.  I did not want to replace the servicestack serialization so I created these plugins to format the text.

I based the json formatting on Mark David Rodgers example at

http://www.markdavidrogers.com/oxitesample/

Here is the origional servicestack post showing example usage
http://stackoverflow.com/questions/13740534/does-servicestack-text-offer-pretty-printing-of-json/16145182#16145182

In the future I expect servicestack will add a format option to their serializers and this code will not be needed.

I still have some unit tests to get working and add a servicestack plugin test I have working in another project but I am currently using the code successfully.
