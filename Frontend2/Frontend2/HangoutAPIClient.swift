//
//  HangoutClient.swift
//  Hangout
//
//  Created by Vahid Mazdeh on 11/7/14.
//  Copyright (c) 2014 Peyman Mortazavi. All rights reserved.
//

import Foundation

public class HangoutClient {
    
    public class var ApiUrl: String{
        get{
            return "http://localhost:8080/"
        }
    }
    
    /*init(apiUrl: String) {
        //ApiUrl = apiUrl
    }*/
    
    /*func testGet () -> String {
        let urlPath: String = ApiUrl + "accounts/test"
        var url: NSURL = NSURL(string: urlPath)!
        var request1: NSURLRequest = NSURLRequest(URL: url)
        var response: AutoreleasingUnsafeMutablePointer <NSURLResponse?
        >=nil
        
        var error: AutoreleasingUnsafeMutablePointer<NSError?> = nil
        var dataVal: NSData? =  NSURLConnection.sendSynchronousRequest(request1, returningResponse: response, error:error)
        if let data = dataVal {
            var err: NSError? = nil
            var jsonResult: NSDictionary = NSJSONSerialization.JSONObjectWithData(data, options: NSJSONReadingOptions.MutableContainers, error: &err) as NSDictionary
            println("Synchronous\(jsonResult)")
            return jsonResult["firstName"] as NSString;
        }
        return "Failed to acquire server data";
    }*/
    
}