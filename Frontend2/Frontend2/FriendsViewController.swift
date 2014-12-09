//
//  FriendsViewController.swift
//  Frontend2
//
//  Created by Ryan Tabler on 12/8/14.
//  Copyright (c) 2014 Ryan Tabler. All rights reserved.
//

import Foundation
import UIKit

class FriendsViewController: UITableViewController {
    
    override func viewDidLoad() {
        super.viewDidLoad()
        /*
        var data = NSMutableData()
        let url = NSURL(string: HangoutClient.ApiUrl + "accounts/friends")!
        var request: NSMutableURLRequest = NSMutableURLRequest(URL: url)
        request.HTTPMethod = "GET"
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")
        */
        let url = NSURL(string: HangoutClient.ApiUrl + "accounts/friends")!
        var request: NSMutableURLRequest = NSMutableURLRequest(URL: url)
        request.HTTPMethod = "GET"
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")
        var response: AutoreleasingUnsafeMutablePointer <NSURLResponse?
        >=nil
        var error: AutoreleasingUnsafeMutablePointer<NSError?> = nil
        var dataVal: NSData? =  NSURLConnection.sendSynchronousRequest(request, returningResponse: response, error:error)
        if let data = dataVal {
            var err: NSError? = nil
            var jsonResult: NSDictionary = NSJSONSerialization.JSONObjectWithData(data, options: NSJSONReadingOptions.MutableContainers, error: &err) as NSDictionary
            let serial = NSJSONSerialization.dataWithJSONObject(jsonResult, options:NSJSONWritingOptions(0), error: nil)
            println(serial)
            let object = NSJSONSerialization.JSONObjectWithData(serial!, options: NSJSONReadingOptions(0), error: nil) as NSDictionary
            println(object)
        } else {
            
        }
        /*
        let url = NSURL(string: HangoutClient.ApiUrl + "accounts/friends")!
        var request: NSMutableURLRequest = NSMutableURLRequest(URL: url)
        request.HTTPMethod = "GET"
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")
        
        let friendsDictionary = [] //"firstname": firstField.text!, "lastname": lastField.text!, "phonenumber":phoneField.text, "email":emailField.text, "password":passwordField.text]
        let data = NSJSONSerialization.dataWithJSONObject(friendsDictionary, options: NSJSONWritingOptions(0), error: nil)
        request.HTTPBody = data
        var response: AutoreleasingUnsafeMutablePointer <NSURLResponse?> = nil
        var error: AutoreleasingUnsafeMutablePointer<NSError?> = nil
        
        var dataVal: NSData? = NSURLConnection.sendSynchronousRequest(request, returningResponse: response, error:error)
        
        if let data = dataVal{
            let json = NSJSONSerialization.JSONObjectWithData(data, options: NSJSONReadingOptions.MutableContainers, error: nil) as NSDictionary
            println(json)
            //let friendsOpt = json["id"] as NSString?
*/
            /*if let friendsId = id {
                //We have an id!
                self.performSegueWithIdentifier("SignupToFriends", sender: self)
                
            } else {
                //We have an error!
            }*/
            //Either use UIAlertView to show error, or call self.presentViewController(the other view controller)
        // Call GetFriendRequests()
        // Format array, plug it into var dataSource
    }
    
    //@IBOutlet weak var dataSource: NSArray!
    //@IBOutlet weak var delegate:
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
    }
    
    @IBAction func dismiss(sender: UIButton) {
        self.dismissViewControllerAnimated(true, completion: nil)
    }
}