import * as React from 'react';
import Accordion from '@mui/material/Accordion';
import AccordionSummary from '@mui/material/AccordionSummary';
import AccordionDetails from '@mui/material/AccordionDetails';
import Typography from '@mui/material/Typography';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import WarningIcon from '@mui/icons-material/Warning';
import Highcharts from 'highcharts';
import HighchartsReact from 'highcharts-react-official';
import highchartsMore from 'highcharts/highcharts-more';
import Axios from "axios";
import { Alert } from '@mui/material';
import Button from '@mui/material/Button';
highchartsMore(Highcharts);


export default function BasicAccordion() {
  const[time,setTime] = React.useState([]);
  const [temp,setTemp] = React.useState([]);
  const[tempRange,setTempRange] = React.useState([]);
  const[tempAlert,setTempAlert] =React.useState(false);

  const [conductivity,setConductivity] = React.useState([]);
  const[conductivityRange,setConductivityRange] = React.useState([]);
  const[conductivityAlert,setConductivityAlert] =React.useState(false);

  const [PH,setPH] = React.useState([]);
  const[PHRange,setPHRange] = React.useState([]);
  const[PHAlert,setPHAlert] =React.useState(false);


  var ranges = [
    [13.7, 25.6],
    [13.3, 21.8],
    [11.2, 19.9],
    [7.9, 17.3],
    [4.9, 20.6],
    [5.1, 16.8],
    [9.3, 21.1],
    [11.1, 20.5],
    [8.9, 18.4],
    [4.6, 23.2],
    [11.5, 26.0],
    [8.6, 23.4],
    [9.8, 22.2],
    [8.1, 18.2],
    [5.9, 20.2],
    [4.5, 20.2],
    [8.9, 19.8],
    [11.1, 22.1],
    [7.9, 26.7],
    [15.9, 28.6],
    [14.9, 27.5],
    [9.5, 26.0],
    [11.5, 22.4],
    [8.6, 21.1],
    [12.9, 21.7],
    [13.6, 20.9],
    [9.6, 23.9],
    [8.6, 22.7],
    [7.5, 25.7],
    [5.5, 24.3],
    [10.4, 21.2]

],
averages = [
    [18.1],
    [17.1],
    [15.2],
    [12.7],
    [13.3],
    [10.6],
    [15.6],
    [16.1],
    [14.0],
    [15.3],
    [17.5],
    [17.5],
    [15.3],
    [13.9],
    [13.7],
    [13.8],
    [14.0],
    [15.8],
    [18.6],
    [21.5],
    [19.8],
    [17.6],
    [16.8],
    [15.6],
    [16.7],
    [16.3],
    [17.2],
    [16.0],
    [16.9],
    [16.1],
    [14.5]
];


var chart1 ={

title: {
    text: 'Example Chart',
    align: 'left'
},

subtitle: {
    text: 'Source: ' +
        '<a href="https://www.yr.no/nb/historikk/graf/1-113585/Norge/Viken/Nesbyen/Nesbyen?q=2022-07"' +
        'target="_blank">YR</a>',
    align: 'left'
},

xAxis: {
    type: 'datetime',
    accessibility: {
        rangeDescription: 'Range: Jul 1st 2022 to Jul 31st 2022.'
    }
},

yAxis: {
    title: {
        text: null
    }
},

tooltip: {
    crosshairs: true,
    shared: true,
    valueSuffix: '°C'
},

plotOptions: {
    series: {
        pointStart: Date.UTC(2022, 6, 1),
        pointIntervalUnit: 'day'
    }
},

series: [{
    name: 'Temperature',
    data: averages,
    type: 'line',
    zIndex: 1,
    marker: {
        fillColor: 'white',
        lineWidth: 2,
        lineColor: Highcharts.getOptions().colors[0]
    }
}, {
    name: 'Range',
    data: ranges,
    type: 'arearange',
    lineWidth: 0,
    linkedTo: ':previous',
    color: Highcharts.getOptions().colors[0],
    fillOpacity: 0.3,
    zIndex: 0,
    marker: {
        enabled: false
    }
}]
};


const chart_temp ={

  title: {
      text: 'Temperature Graph',
      align: 'middle'
  },
  
  xAxis: {
    title:{
      
      text:'time'
    },
    categories: time
  },
  
  
  
  yAxis: {
      title: {
          text: null
      }
  },
  
  tooltip: {
      crosshairs: true,
      shared: true,
      valueSuffix: '°C'
  },
  
  series: [{
      name: 'Temperature',
      data: temp/*[points]*/,
      type: 'line',
      zIndex: 1,
      zones: [{
        value: 16,
        color:'red'
      },{
        value:20,
        color:'green'
      },
      {
        value:100,
        color:'red'
      }],
      marker: {
          fillColor: 'white',
          lineWidth: 2,
          lineColor: Highcharts.getOptions().colors[0]
      },
   }, {
       name: 'Range',
       data: tempRange,
       type: 'arearange',
       lineWidth: 0,
       linkedTo: ':previous',
       color: Highcharts.getOptions().colors[0],
       fillOpacity: 0.3,
       zIndex: 0,
       marker: {
           enabled: false
       }
   }]

  };

  const chart_conductivity ={

    title: {
        text: 'Conductivity Graph',
        align: 'middle'
    },
    
    xAxis: {
      title:{
        
        text:'time'
      },
      categories: time
    },
    
    yAxis: {
        title: {
            text: null
        }
    },
    
    tooltip: {
        crosshairs: true,
        shared: true,
        valueSuffix: ' PPM'
    },
    
    series: [{
        name: 'Conductivity',
        data: conductivity/*[points]*/,
        type: 'line',
        zIndex: 1,
        zones: [{
          value: 20,
          color:'red'
        },{
          value:40,
          color:'green'
        },
        {
          value:60,
          color:'red'
        }],
        marker: {
            fillColor: 'white',
            lineWidth: 2,
            lineColor: Highcharts.getOptions().colors[0]
        },
     }, {
         name: 'Range',
         data: conductivityRange,
         type: 'arearange',
         lineWidth: 0,
         linkedTo: ':previous',
         color: Highcharts.getOptions().colors[0],
         fillOpacity: 0.3,
         zIndex: 0,
         marker: {
             enabled: false
         }
     }]
  
    };

    const chart_PH ={

      title: {
          text: 'PH Graph',
          align: 'middle'
      },
      
      xAxis: {
        title:{
          
          text:'time'
        },
        categories: time
      },
      
      
      
      yAxis: {
          title: {
              text: null
          }
      },
      
      tooltip: {
          crosshairs: true,
          shared: true,
          valueSuffix: ''
      },
      
      series: [{
          name: 'PH',
          data: PH/*[points]*/,
          type: 'line',
          zIndex: 1,
          zones: [{
            value: 6,
            color:'red'
          },{
            value:9,
            color:'green'
          },
          {
            value:25,
            color:'red'
          }],
          marker: {
              fillColor: 'white',
              lineWidth: 2,
              lineColor: Highcharts.getOptions().colors[0]
          },
       }, {
           name: 'Range',
           data: PHRange,
           type: 'arearange',
           lineWidth: 0,
           linkedTo: ':previous',
           color: Highcharts.getOptions().colors[0],
           fillOpacity: 0.3,
           zIndex: 0,
           marker: {
               enabled: false
           }
       }]
    
      };

//get_request();



  async function  get_request(){
    await Axios({url:'https://localhost:7108/api/sensors', headers: {
      //'Access-Control-Allow-Origin' : '*',
     // 'Access-Control-Allow-Methods':'GET,PUT,POST,DELETE,PATCH,OPTIONS',
   }})
     .then((response)=>{
       
       console.log(response);
       var temp_time=[]
       var temp_temp=[],tmp_conductivity=[], tmp_PH=[];
       var tmp_tempRange=[],tmp_conductivityRange=[], tmp_PHRange=[];
       response.data.forEach(element => {

      temp_time.push(element.time.substring(11));

       temp_temp.push(element.temperature);
       tmp_conductivity.push(element.conductivity);
       tmp_PH.push(element.ph)

       tmp_tempRange.push([16,20]);
       tmp_conductivityRange.push([20,40]);
       tmp_PHRange.push([6,9]);
       
     });
    
    setTime(temp_time);
    setTemp(temp_temp);
    setConductivity(tmp_conductivity);
    setPH(tmp_PH);


    setTempRange(tmp_tempRange);
    setConductivityRange(tmp_conductivityRange);
    setPHRange(tmp_PHRange);

     render_chart();
   })
     
   }
   function render_chart(){
    console.log(chart_PH)
    //set flag for each to display alert
    console.log(temp.length)
    for(var i=0;i<temp.length;i++){
      console.log('in loop ', temp[i],tempRange[i])
      if (temp[i]<tempRange[i][0]||temp[i]>tempRange[i][1]){
        console.log('temp updated')
        setTempAlert(true);
      }
      if (conductivity[i]<conductivityRange[i][0]||conductivity[i]>conductivityRange[i][1]){
        console.log('temp updated')
        setConductivityAlert(true);
      }
      if (PH[i]<PHRange[i][0]||PH[i]>PHRange[i][1]){
        console.log('temp updated')
        setPHAlert(true);
      }
    }
    //console.log(chart1)
  }
  React.useEffect(()=>{
    console.log(temp)
  },[temp])

   //get_request();
  return (
    
    <div 
    style = {{
      
   }}>
      <h1>Aquasync Analytics User Interface Demo</h1>
      <Accordion onChange={()=>get_request()}>
        <AccordionSummary
          expandIcon={<ExpandMoreIcon />}
          aria-controls="panel1a-content"
          id="panel1a-header"
        >
          <Typography>PH</Typography>
        </AccordionSummary>
        <AccordionDetails>
        
        <HighchartsReact
          highcharts={Highcharts}
          options={chart1}
        />
          <Typography>
            
          </Typography>
        </AccordionDetails>
      </Accordion>
      <Accordion onChange={()=>get_request()} >
        <AccordionSummary
          expandIcon={<ExpandMoreIcon />}
          aria-controls="panel2a-content"
          id="panel2a-header"
        >
          <Typography>Temperature {tempAlert && <WarningIcon/>}</Typography>
        </AccordionSummary>
        <AccordionDetails>
        <Button variant="contained" color="secondary">Update</Button>

        <HighchartsReact
          highcharts={Highcharts}
          options={chart_temp}
        />
          
        <Typography>
           
          </Typography>
        </AccordionDetails>
      </Accordion>
      <Accordion onChange={()=>get_request()}>
        <AccordionSummary
          expandIcon={<ExpandMoreIcon />}
          aria-controls="panel2a-content"
          id="panel2a-header"
        >
          <Typography>Conductivity {conductivityAlert && <WarningIcon/>}</Typography>
        </AccordionSummary>
        <AccordionDetails>
        
        <Button variant="contained" color="secondary">Update</Button>

        <HighchartsReact
          highcharts={Highcharts}
          options={chart_conductivity}
        />
       
        </AccordionDetails>
      </Accordion>
      <Accordion onChange={()=>get_request()}>
        <AccordionSummary
          expandIcon={<ExpandMoreIcon />}
          aria-controls="panel2a-content"
          id="panel2a-header"
        >
          <Typography>PH {PHAlert && <WarningIcon/>}</Typography>
        </AccordionSummary>
        <AccordionDetails>
        
        <Button variant="contained" color="secondary">Update</Button>

        <HighchartsReact
          highcharts={Highcharts}
         options={chart_PH}
        />
       
        </AccordionDetails>
      </Accordion>
    </div>
  );
}
